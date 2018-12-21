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

