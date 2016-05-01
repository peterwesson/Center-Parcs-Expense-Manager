var PaymentGroup = React.createClass({
    render: function() {
        var users = this.props.data.Users.map(function(user) {
            return (<Person data={user} />);
        });

var balanceString = (this.props.data.Balance < 0) ? (<span>-£{(-this.props.data.Balance).toFixed(2)}</span>) : (<span>£{this.props.data.Balance.toFixed(2)}</span>);

        return (
            <tr>
                <td width="180px">
                    {users}
                </td>
                <td>
                    {balanceString}
                </td>
            </tr>
        );
    }
});