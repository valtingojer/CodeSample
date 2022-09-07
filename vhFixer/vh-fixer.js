var vhFixer = (function () {
    var exec = function () {
        let vh = window.innerHeight * 0.01;
        let vhpx = vh + 'px';
        document.documentElement.style.setProperty('--vh', vhpx);
    }

    var listenForResize = function () {
        window.addEventListener('resize', exec);
    }

    var createVhClasses = function () {

        var steps = [25, 50, 75, 100];

        var style = document.createElement('style');
        style.type = 'text/css';

        var styleContent = '';

        for (let i = 0; i < steps.length; i++) {
            let step = steps[i];
            styleContent += '.vh-' + step + ' { height: ' + step + 'vh; height: calc(var(--vh, 1vh) * ' + step + '); }';
        }

        style.innerHTML = styleContent;

        document.head.appendChild(style);
    }

    var init = function () {
        exec();
        createVhClasses();
        listenForResize();
    }

    return {
        init: init
    }
})();

document.addEventListener('DOMContentLoaded', vhFixer.init);