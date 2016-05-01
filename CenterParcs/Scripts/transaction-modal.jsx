var TransactionModal = React.createClass({
    handleDeleteTransaction: function(e) {
        e.preventDefault();

        var modalId = this.props.modalId;

        var update = this.props.update;

        $.ajax({
            url: "/transaction/deletetransaction",
            data: { transactionId: this.props.data.TransactionId },
            type: "POST",
            success: function (response) {
                $('#' + modalId).modal('hide');
                update();
            }
        });
    },
    render: function() {
        var transaction = this.props.data;
        var modalId = this.props.modalId;
        var updateModalId = "update-transaction-modal" + transaction.TransactionId;
        var update = this.props.update;

        var subtransactions = transaction.SubTransactions.map(function(subTransaction) {
            return (
                <div className="row subtransaction-row">
                    <div className="col-xs-3">
                        <Person data={subTransaction.User} />
                    </div>
                    <div className="col-xs-5 subtransaction-text">
                        {subTransaction.User.FullName}
                    </div>
                    <div className="col-xs-4 subtransaction-text">
                        £{subTransaction.Amount.toFixed(2)}
                    </div>
                </div>
            );
        });

        var openUpdateModal = function() {
            $("#" + updateModalId).modal("show");
        }

        var buttons = <div></div>;

        if (transaction.User.Id === userId || userId === "16afe884-6fd8-486f-b0c7-7685e1db9f31") {
            buttons =   <div class="btn-group" role="group">
                            <button type="button" className="btn btn-primary" onClick={openUpdateModal}>Update</button>
                            <div id={updateModalId} className="modal fade text-left" role="dialog">
                                <div className="vertical-alignment-helper">
                                    <div className="modal-dialog vertical-align-center">
                                        <UpdateTransactionModal data={transaction} update={update} updateModalId={updateModalId}/>
                                    </div>
                                </div>
                            </div>&nbsp;
                            <form onSubmit={this.handleDeleteTransaction} className="inline-form">
                                <button type="submit" className="btn btn-danger" value="post">Delete</button>
                            </form>
                        </div>;
        }

        return (
            <div className="text-center">
                <h3>{transaction.TransactionDescription}</h3>
                <h5>Created by {transaction.User.FullName}</h5>
                <PersonLarge data={transaction.User} />
                <h4>Amount: £{transaction.Amount.toFixed(2)}</h4>
                <div className="container">
                    {subtransactions}
                </div>
                {buttons}
            </div>
        );
    }
});
