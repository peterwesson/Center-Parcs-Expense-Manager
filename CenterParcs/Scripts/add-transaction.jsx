var AddTransaction = React.createClass({
    handleAddTransaction: function(e) {
        e.preventDefault();

        var description = ReactDOM.findDOMNode(this.refs.description).value.trim();
        var amount = ReactDOM.findDOMNode(this.refs.amount).value.trim();


        var subtransactions = this.refs.subtransactions.getSubTransactions();

        var transaction = {
            Amount: amount,
            TransactionDescription: description,
            SubTransactions: subtransactions
        }

        var update = this.props.update;

        if (description === "") {
            alert("You must enter a description");

            return;
        }

        if (subtransactions.length === 0) {
            alert("You must include at least one person");

            return;
        }

        $.ajax({
            url: "/transaction/addtransaction",
            data: JSON.stringify(transaction),
            contentType: "application/json; charset=utf-8",
            type: "POST",
            success: function (response) {
                $('#add-transaction-modal').modal('hide');
                update();
            }
        });
    },
    render: function() {
        var url = this.props.url;

        return (
            <div className="text-center">
                <button type="button" className="glyphicon glyphicon-plus btn btn-success" data-toggle="modal" data-target="#add-transaction-modal"></button>
                <div id="add-transaction-modal" className="modal fade text-left" role="dialog">
                    <div className="vertical-alignment-helper">
                        <div className="modal-dialog vertical-align-center">
                            <div className="modal-dialog">
                                <div className="modal-content">
                                    <form onSubmit={this.handleAddTransaction} >
                                        <div className="modal-header">
                                            <button type="button" className="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                            <h4 className="modal-title" id="myModalLabel">Add Transaction</h4>
                                        </div>
                                        <div className="modal-body">
                                            <div className="form-group">
                                                <label for="transaction-description">Description</label>
                                                <input type="text" className="form-control" id="transaction-description" placeholder="Enter a description here" ref="description" />
                                            </div>
                                            <div className="form-group">
                                                <label for="transaction-amount">Amount</label>
                                                <div className="input-group">
                                                    <span className="input-group-addon">£</span>
                                                    <input type="number" className="form-control" id="transaction-amount" placeholder="0.00" ref="amount" step="0.01" min="0" />   
                                                </div>
                                            </div>
                                            <AddSubTransactions id="user-select" ref="subtransactions" url={url} transaction={this}/>
                                        </div>
                                        <div className="modal-footer">
                                            <center>
                                                <button type="submit" className="btn btn-success" value="Post">Add</button>
                                            </center>
                                        </div>
                                    </form>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
});