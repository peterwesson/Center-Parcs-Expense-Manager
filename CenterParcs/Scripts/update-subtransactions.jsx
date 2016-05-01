var UpdateSubTransactions = React.createClass({
    getUsers: function() {
        var xhr = new XMLHttpRequest();
        xhr.open('get', this.props.url, true);
        xhr.onload = function() {
            var data = JSON.parse(xhr.responseText);
            retrocycle(data);
            this.setState({ users: data });
            var subtransactions = [];
            for (var i = 0; i < data.length; i++) {
                var amount = 0;
                var include = false;

                for (var j = 0; j < this.props.data.length; j++) {
                    if (this.props.data[j].User.Id === data[i].Id) {
                        amount = this.props.data[j].Amount.toFixed(2);
                        include = true;
                    }
                }

                subtransactions.push({
                    User: data[i],
                    Amount: amount,
                    Include: include
                });
                
            }
            this.setState({ subtransactions: subtransactions });
        }.bind(this);
        xhr.send();
    },
    getInitialState: function() {
        return {
            users: [],
            subtransactions: []
        };
    },
    componentDidMount: function() {
        this.getUsers();
    },
    getSubTransactions: function() {
        var subtransactions = [];
        for (var i = 0; i < this.state.subtransactions.length; i++) {
            subTransactionNode = this.refs["subtransaction-" + i];

            var amount = ReactDOM.findDOMNode(subTransactionNode.refs.amount).value.trim();
            var include = ReactDOM.findDOMNode(subTransactionNode.refs.include).checked;
            var transactionId = 0;

            for (var j = 0; j < this.props.data.length; j++) {
                if (this.props.data[j].User.Id === this.state.subtransactions[i].User.Id) {
                    transactionId = this.props.data[j].TransactionId;
                }
            }

            if (include) {
                subtransactions.push({
                    Amount: amount,
                    UserId: this.state.users[i].Id,
                    TransactionId: transactionId
                });
            }
        }

        return subtransactions;
    },
    selectAllHandle: function() {
        var includeCount = 0;
        for (var i = 0; i < this.state.subtransactions.length; i++) {
            subTransactionNode = this.refs["subtransaction-" + i];
            if (subTransactionNode.state.addSubtransactionInclude) {
                includeCount++;
            }

            subTransactionNode.setState({ addSubtransactionInclude : true });
        }

        if (includeCount === this.state.subtransactions.length) {
            for (var i = 0; i < this.state.subtransactions.length; i++) {
                subTransactionNode = this.refs["subtransaction-" + i];

                subTransactionNode.setState({ addSubtransactionInclude : false });
            }
        }
    },
    splitHandle: function() {
        var amount = ReactDOM.findDOMNode(this.props.transaction.refs.amount).value.trim();
        var includeCount = 0;

        for (var i = 0; i < this.state.subtransactions.length; i++) {
            subTransactionNode = this.refs["subtransaction-" + i];

            if (subTransactionNode.state.addSubtransactionInclude) {
                includeCount++;
            }
        }

        for (var i = 0; i < this.state.subtransactions.length; i++) {
            subTransactionNode = this.refs["subtransaction-" + i];

            subTransactionNode.setState({ addSubtransactionAmount : Math.round(100 * amount / includeCount) / 100 });
        }
    },
    render: function() {
        subTransactions = this.state.subtransactions.map(function(subTransaction, index) {
            return (<AddSubTransaction data={subTransaction} ref={"subtransaction-" + index} />);
});
        
return (
    <div className="container">
        <div className="row">
            <div className="col-xs-6">
                <button type="button" className="btn btn-primary" onClick={this.selectAllHandle}>Select All</button>
            </div>
            <div className="col-xs-6">
                <button type="button" className="btn btn-primary" onClick={this.splitHandle}>Split Equally</button>
            </div>
        </div>
        <br />
        {subTransactions}
    </div>
        );
}
});