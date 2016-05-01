var Summary = React.createClass({
    getPaymentGroups: function() {
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
        this.getPaymentGroups();
    },
    render: function() {
        var paymentGroups = this.state.data.map(function(paymentGroup) {
            return (<PaymentGroup data={paymentGroup} />);
        });
        return (
            <table className="table table-striped">
                <thead>
                    <tr>
                        <th>
                        </th>
                        <th>
                            Amount
                        </th>
                    </tr>
                </thead>
                <tbody>
                    {paymentGroups}
                </tbody>
            </table>
        );
    }
});

ReactDOM.render(<Summary url="GetAllPaymentGroups" />, document.getElementById('summary'));