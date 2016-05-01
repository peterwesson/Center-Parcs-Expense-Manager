var Person = React.createClass({
    render: function() {
        var user = this.props.data;
        return (
            <img src={"../Content/" + user.UserName + ".jpg"} className="person"/>
        );
    }
});

var PersonLarge = React.createClass({
    render: function() {
        var user = this.props.data;
        return (
            <img src={"../Content/" + user.UserName + ".jpg"} className="person person-large"/>
        );
    }
});

var PersonSmall = React.createClass({
    render: function() {
        var user = this.props.data;
        return (
            <img src={"../Content/" + user.UserName + ".jpg"} className="person person-small"/>
        );
    }
});