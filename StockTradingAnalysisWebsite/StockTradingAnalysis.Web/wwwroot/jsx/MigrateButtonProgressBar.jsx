class MigrateButtonProgressBar extends React.Component {
    constructor(props) {
        super(props);

        this.state = { progressbarValue: 0, popup: { message: '', type: '' }, steps: 12, enabled: true, baseUrl: '/Migration/Migrate/' };

        this.updateProgressBar = this.updateProgressBar.bind(this);
        this.generate = this.generate.bind(this);
    }

    componentDidMount() {

    }

    handleClick() {
        this.setState({ enabled: !this.state.enabled });
        this.setState({ progressbarValue: 0 });

        this.generate(0)
            .then(() => this.generate(1))
            .then(() => this.generate(2))
            .then(() => this.generate(3))
            .then(() => this.generate(4))
            .then(() => this.generate(5))
            .then(() => this.generate(6))
            .then(() => this.generate(7))
            .then(() => this.generate(8))
            .then(() => this.generate(9))
            .then(() => this.generate(10))
            .then(() => this.generate(11))
            .then(this.setState({ enabled: !this.state.enabled }));
    }

    generate(step) {
        return axios.get(this.state.baseUrl + step)
            .then(function (res) {
                this.updateProgressBar(step);
                this.valid = res.data.unique;
                this.setState({ popup: { message: res.data, type: 'success' } });
            }.bind(this))
            .catch(function (res) {
                this.setState({ progressbarValue: 0, popup: { message: res.data, type: 'danger' } });
            }.bind(this));
    }

    updateProgressBar(step) {
        this.setState({ progressbarValue: (Math.round(((step + 1) / this.state.steps) * 100)) });
    }

    render() {
        let disabled = this.state.enabled ? '' : 'disabled';
        return (
            <div>
                <p className="lead">
                    <button className="btn btn-danger btn-lg" {... { disabled }} onClick={this.handleClick.bind(this)} role="button">Start</button>
                </p>
                <div className="progress">
                    <div className="progress-bar progress-bar-striped progress-bar-animated" role="progressbar" style={{ width: this.state.progressbarValue + '%' }}></div>
                </div>
                <Popup message={this.state.popup.message} type={this.state.popup.type} />
            </div>
        );
    }
}

ReactDOM.render(<MigrateButtonProgressBar />, document.getElementById('button-migrate'));