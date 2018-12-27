class Popup extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        if (!this.props.message || !this.props.type)
            return null;

        return (
            <div className={`container-fluid fixed-bottom`} style={{ opacity: '0.6' }}>
                <div className="row">
                    <div className="col-lg-12">
                        <div className={`alert alert-${this.props.type}`}>
                            <button type="button" className="close"></button>
                            {this.props.message}
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}


{/* ES6 syntax */ }

class UpdateQuotationsButton extends React.Component {
    constructor(props) {
        super(props);

        const {
            id = document.getElementById('button-update-quotations').getAttribute("data-id"),
            wkn = document.getElementById('button-update-quotations').getAttribute("data-wkn")
        } = props;

        this.state = {
            serviceAvailable: false,
            ids: { id, wkn },
            popup: { message: '', type: '' }
        };

        /* Enable status push of service */
        var hub = $.connection.quotationHub;

        /*$.connection.hub.logging = true;*/
        $.connection.hub.start();

        hub.client.SendQuotationServiceStatus = function (status) {
            this.setState({ serviceAvailable: status });
        }.bind(this);
    }

    componentDidMount() {
        this.updateServiceAvailability();
    }

    fadeout() {
        this.setState({ popup: { message: '', type: '' } });
    }

    handleClick() {
        this.setState({ popup: { message: 'Aktualisierung gestartet ...', type: 'info' } });

        if (this.state.ids.id) {        
            axios.post('/Quotation/UpdateQuotation', { id: this.state.ids.id })
                .then(function (res) {
                    this.setState({ popup: { message: res.data.Message, type: 'success' } });
                }.bind(this))
                .catch(function (res) {
                    this.setState({ popup: { message: res.data.Message, type: 'danger' } });
                }.bind(this));
        }
        else if (this.state.ids.wkn) {            
            axios.post('/Quotation/UpdateQuotationByWkn', { wkn: this.state.ids.wkn })
                .then(function (res) {
                    this.setState({ popup: { message: res.data.Message, success: 'success' } });
                }.bind(this))
                .catch(function (res) {
                    this.setState({ popup: { message: res.data.Message, success: 'danger' } });
                }.bind(this));            
        }

        setTimeout(this.fadeout.bind(this), 10000); 
    }

    updateServiceAvailability() {
        axios.get('/Quotation/IsQuotationServiceOnline')
            .then(res => {
                this.setState({ serviceAvailable: res.data });
            });
    }

    render() {
        let disabled = this.state.serviceAvailable ? '' : 'disabled';
        return (
            <div className="btn-group">
                <Popup message={this.state.popup.message} type={this.state.popup.type} />
                <button className="btn btn-secondary" type="button" {... { disabled } } onClick={this.handleClick.bind(this)}>Aktienkurse aktualisieren</button>
            </div>
        );
    }
}

ReactDOM.render(<UpdateQuotationsButton />, document.getElementById('button-update-quotations'));
class GenerateTestDataButtonProgressBar extends React.Component {
    constructor(props) {
        super(props);

        this.state = { progressbarValue: 0, popup: { message: '', type: '' }, steps: 8, enabled: true, baseUrl: '/TestData/Generate/' };

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
                    <button className="btn btn-danger btn-lg" {... { disabled } } onClick={this.handleClick.bind(this)} role="button">Start</button>
                </p>
                <div className="progress">
                    <div className="progress-bar progress-bar-striped progress-bar-animated" role="progressbar" style={{ width: this.state.progressbarValue + '%' }}></div>
                </div>
                <Popup message={this.state.popup.message} type={this.state.popup.type} />
            </div>
        );
    }
}

ReactDOM.render(<GenerateTestDataButtonProgressBar />, document.getElementById('button-generate-test-data'));