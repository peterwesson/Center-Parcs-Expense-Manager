var Transactions = React.createClass({
    getTransactions: function() {
        var xhr = new XMLHttpRequest();
        xhr.open('get', this.props.url, true);
        xhr.onload = function() {
            var data = JSON.parse(xhr.responseText);
            retrocycle(data);
            this.setState({ data: data });
        }.bind(this);
        xhr.send();
    },
    getInitialState: function() {
        return {
            data: []
        };
    },
    componentDidMount: function() {
        this.getTransactions();
    },
    render: function() {
        var update = this.getTransactions;
        var transactions = this.state.data.map(function(transaction) {
            return (<Transaction data={transaction} update={update} />);
        });

        return (
            <div>
                <AddTransaction url={"Transaction/GetAllUsers"} update={this.getTransactions} />
                <br/>
                <div className="container">
                    <div className="row">
                        <div className="col-xs-3">
                            <h4>Creator</h4>
                        </div>
                        <div className="col-xs-5">
                            <h4>Description</h4>
                        </div>
                        <div className="col-xs-4">
                            <h4>Amount</h4>
                        </div>
                    </div>
                    <div className="row">
                        {transactions}
                    </div>
                </div>
            </div>
        );
    }
});

ReactDOM.render(<Transactions url="Transaction/GetAllTransactions" />, document.getElementById('transactions'));