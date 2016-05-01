var AddSubTransaction = React.createClass({
    onAmountChange: function(e) {
        this.setState({ addSubtransactionAmount: e.target.value });
    },
    onIncludeChange: function(e) {
        this.setState({ addSubtransactionInclude: !this.state.addSubtransactionInclude });
    },
    getInitialState: function() {
        return {
            addSubtransactionAmount: this.props.data.Amount,
            addSubtransactionInclude: this.props.data.Include
        };
    },
    render: function() {
        var subTransaction = this.props.data;

        return (
            <div className="row">
                <div className="col-xs-2">
                    <input type="checkbox" className="form-control subtransaction-checkbox" ref="include" checked={this.state.addSubtransactionInclude} onChange={this.onIncludeChange} />
                </div>
                <div className="col-xs-3">
                    <Person data={subTransaction.User} />
                </div>
                <div className="col-xs-7 subtransaction-text">
                    <div className="input-group">
                        <span className="input-group-addon">£</span>
                        <input type="number" className="form-control" id="transaction-amount" placeholder="0.00" ref="amount" step="0.01" min="0" value={this.state.addSubtransactionAmount} onChange={this.onAmountChange} /> 
                    </div>
                </div>
            </div>
        );
    }
});
