var SubTransactions = React.createClass({
    render: function() {
        var subtransactions = this.props.data.map(function(subTransaction) {
            return (<PersonSmall data={subTransaction.User} />);
        });

        return (
            <div className="row text-right">
                <div className="col-xs-12">
                    {subtransactions}
                </div>
            </div>
        );
    }
});