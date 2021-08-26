import React from "react";
import ReactDOM from "react-dom";
import * as AppConstants from "../config/constants";

const modalRoot = document.getElementById(AppConstants.ModalRoot);

class Modal extends React.Component {
  constructor (props)
  {
      super(props);
      this.el = document.createElement("div");
  }
  
  componentDidMount() {
    console.log("mounting portal");
    modalRoot.appendChild(this.el);
  }

  componentWillUnmount() {
    console.log("unmounting portal");
    modalRoot.removeChild(this.el);
  }

  render() {
    return ReactDOM.createPortal(this.props.children, this.el);
  }
}

export default Modal;