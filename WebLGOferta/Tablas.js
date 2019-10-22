function Tables(idcontainer, colums, rows, list, head, flist) {

    var lista = list;
    var header = head;
    var filas = rows;
    var cantCols = colums;
    var pos0 = 0;
    var container0 = document.getElementById(idcontainer);
    var makeList = flist;
    ConstruirTabla();

    function ConstruirTabla() {
        if (lista.length == 0) {
            container0.innerHTML = ""; return;
        }
        var text = "<table class='tableList'>";
        text += "<tr style='background-color:maroon;color:white;'>" + header + "</tr>";
        text += makeList(pos0, lista, filas);
        text += makeFooter();
        text += "</table>";
        container0.innerHTML = text;
        makeEvents();
    }
    function makeFooter() {
        var text = "<tr><td colspan='" + cantCols + "'>";
        text += "<table class='tablaFooter'> <tr>";
        text += "<td><div class='bt'>&lt;&lt;</div></td>";
        text += "<td><div class='bt'>&lt;</div></td>";
        text += "<td><div class='bt'>&gt;</div></td>";
        text += "<td><div class='bt'>&gt;&gt;</div></td>";
        text += "</tr></table></td></tr>";
        return text;
    }
    function makeEvents() {
        var botones = container0.getElementsByClassName('bt');
        if (lista.length <= filas) {
            for (var i = 0; i < botones.length; i++) {
                botones[i].style.display = 'none';
            }
        }
        if (pos0 >= lista.length - filas) {
            botones[2].style.display = 'none';
            botones[3].style.display = 'none';
        }
        if (pos0 == 0) {
            botones[0].style.display = 'none';
            botones[1].style.display = 'none';

        }
        botones[0].onclick = function () {

            pos0 = 0; ConstruirTabla();

        }
        botones[1].onclick = function () {

            pos0 -= filas;
            if (pos0 < 0) { pos0 = 0; }
            ConstruirTabla();
        }
        botones[2].onclick = function () {

            pos0 += filas;
            if (pos0 > lista.length - filas) { pos0 = lista.length - filas; }
            ConstruirTabla();
        }
        botones[3].onclick = function () {
            pos0 = lista.length - filas;
            ConstruirTabla();

        }

        onMouseWheel(container0, fDelta)
    }
    //MOUSEWHEEL
    function fDelta(delta) {
        if (delta > 0) { pos0--; if (pos0 < 0) pos0 = 0; ConstruirTabla(); }
        else {
            if (pos0 < lista.length - filas) { pos0++; ConstruirTabla(); }
        }
    }
    function onMouseWheel(element, fDelta) {
        if (element.addEventListener) element.addEventListener('DOMMouseScroll', wheel, false);//Mozilla
        element.onmousewheel = wheel;//IE Opera

        function wheel(event) {
            var delta = 0;
            if (!event) /* For IE. */
                event = window.event;
            if (event.wheelDelta) { /* IE/Opera. */
                delta = event.wheelDelta / 120;
            } else if (event.detail) { /** Mozilla case. */
                /** In Mozilla, sign of delta is different than in IE.
                 * Also, delta is multiple of 3.
                 */
                delta = -event.detail / 3;
            }
            /** If delta is nonzero, handle it.
             * Basically, delta is now positive if wheel was scrolled up,
             * and negative, if wheel was scrolled down.
             */
            if (delta)
                fDelta(delta);
            /** Prevent default actions caused by mouse wheel.
             * That might be ugly, but we handle scrolls somehow
             * anyway, so don't bother here..
             */
            if (event.preventDefault)
                event.preventDefault();
            event.returnValue = false;
        }

    }

}