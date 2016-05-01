var Transaction = React.createClass({
    render: function() {
        var transaction = this.props.data;

        var modalId = "transactionmodal-" + transaction.TransactionId;

        var update = this.props.update;
        return (
            <div>
                <div className="container transaction" data-toggle="modal" data-target={"#" + modalId}>
                    <div className="row">
                        <div className="col-xs-3 text-center"><Person data={transaction.User} /></div>
                        <div className="col-xs-5">{transaction.TransactionDescription}</div>
                        <div className="col-xs-4">£{transaction.Amount.toFixed(2)}</div>
                    </div>
                    <SubTransactions data={transaction.SubTransactions} />
                </div>
                <div id={modalId} className="modal fade text-left" role="dialog">
                    <div className="vertical-alignment-helper">
                        <div className="modal-dialog vertical-align-center">
                            <div className="modal-content">
                                <div className="modal-body">
                                    <button type="button" className="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                    <TransactionModal data={transaction} modalId={modalId} update={update} />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
      );
    }
});