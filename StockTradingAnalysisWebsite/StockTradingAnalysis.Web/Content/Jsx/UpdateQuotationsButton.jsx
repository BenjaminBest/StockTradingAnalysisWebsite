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

        this.updateServiceAvailability();
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
            <div>
                <Popup message={this.state.popup.message} type={this.state.popup.type} />
                <button className="btn btn-secondary" type="button" {... { disabled } } onClick={this.handleClick.bind(this)}>Aktienkurse aktualisieren</button>
            </div>
        );
    }
}

ReactDOM.render(<UpdateQuotationsButton />, document.getElementById('button-update-quotations'));