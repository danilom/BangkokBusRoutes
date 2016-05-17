var busfinder;
(function (busfinder) {
    function start() {
        // Fill the table
        var table = jQuery('#lines-tbody');
        for (var _i = 0; _i < BusData.length; _i++) {
            var bl = BusData[_i];
            var stations0 = bl.directions[0] ?
                bl.directions[0].stations.join(" | ") : "";
            var stations1 = bl.directions[1] ?
                bl.directions[1].stations.join(" | ") : "";
            table.append(jQuery("<tr>")
                .append(jQuery("<th>").text(bl.id))
                .append(jQuery("<td>").text(stations0))
                .append(jQuery("<td>").text(stations1)));
        }
        //content.text(JSON.stringify(BusData));
        /*
        lineid.keyup(() => {
            content.text("");

            let query = lineid.val();
            content.append("query: " + query);


            for (const bl of BusData) {
                //content.append("bl.id: " + bl.id + "; ");
                if (bl.id.indexOf(query) == 0) {
                    //content.append("***");
                    content.append("line: " + bl.id + "; ");
                }
            }
        })
        */
    }
    busfinder.start = start;
})(busfinder || (busfinder = {}));
window.onload = function () {
    busfinder.start();
};
//# sourceMappingURL=app.js.map