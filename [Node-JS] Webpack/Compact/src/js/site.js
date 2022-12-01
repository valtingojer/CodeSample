// import * as some from './some-module.js';
import "../scss/style.scss";

document.addEventListener('DOMContentLoaded', () => {
    console.log('DOM loaded');
    import("./some-module.js").then((some) => {
        console.log('some loaded', some);
        alert('some loaded');
    });
    // console.log("some module was already loaded", some);
});