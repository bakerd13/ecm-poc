(function () {
    window.addEventListener("load", function () {
        this.setTimeout(function () {
            console.log("YES HERE");
            var logo = document.getElementsByClassName("link");

            logo[0].children[0].alt = "Next";
            logo[0].children[0].src = "/logo.png";
        }
        );
    });
})();