var UpdateTransactionModal = React.createClass({
    handleUpdateTransaction: function(e) {
        e.preventDefault();

        var description = ReactDOM.findDOMNode(this.refs.description).value.trim();
        var amount = ReactDOM.findDOMNode(this.refs.amount).value.trim();

        var subtransactions = this.refs.subtransactions.getSubTransactions();

        var transaction = {
            Amount: amount,
            TransactionDescription: description,
            SubTransactions: subtransactions,
            TransactionId: this.props.data.TransactionId
        }

        var update = this.props.update;
        var updateModalId = this.props.updateModalId;

        if (description === "") {
            alert("You must enter a description");

            return;
        }

        if (subtransactions.length === 0) {
            alert("You must include at least one person");

            return;
        }

        $.ajax({
            url: "/transaction/updatetransaction",
            data: JSON.stringify(transaction),
            contentType: "application/json; charset=utf-8",
            type: "POST",
            success: function (response) {
                $('#' + updateModalId).modal('hide');
                update();
            }
        });
    },
    onAmountChange: function(e) {
        this.setState({ amount: e.target.value });
    },
    onDescriptionChange: function(e) {
        this.setState({ description: e.target.value });
    },
    getInitialState: function() {
        return {
            amount: this.props.data.Amount.toFixed(2),
            description: this.props.data.TransactionDescription
        };
    },
    render: function() {
        var transaction = this.props.data;
        
        return (
            <div className="modal-content">
                <div className="modal-header">
                    <button type="button" className="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 className="modal-title" id="myModalLabel">Update Transaction</h4>
                </div>
                <form onSubmit={this.handleUpdateTransaction}>
                    <div className="modal-body">
                        <div className="form-group">
                            <label for="transaction-description">Description</label>
                            <input type="text" value={this.state.description} onChange={this.onDescriptionChange} className="form-control" id="transaction-description" placeholder="Enter a description here" ref="description"/>
                        </div>
                        <div className="form-group">
                            <label for="transaction-amount">Amount</label>
                            <div className="input-group">
                                <span className="input-group-addon">£</span>
                                <input type="number" value={this.state.amount} onChange={this.onAmountChange} className="form-control" id="transaction-amount" placeholder="0.00" ref="amount" step="0.01" min="0"/>   
                            </div>
                        </div>
                        <UpdateSubTransactions url={"Transaction/GetAllUsers"} id="user-select" data={this.props.data.SubTransactions} ref="subtransactions" transaction={this}/>
                    </div>
                    <div className="modal-footer">
                        <center>
                            <button type="submit" className="btn btn-primary" value="post">Update</button>
                        </center>
                    </div>
                </form>
            </div>
        );
    }
});
