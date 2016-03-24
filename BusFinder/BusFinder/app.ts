
declare var BusData: busfinder.ILine[];

module busfinder {


    export interface ILine {
        id: string;
        directions: ILineDirection[];
    }

    export interface ILineDirection {
        direction: string;
        stations: string[];
    }

    export function start() {
        // Fill the table
        const table = jQuery('#lines-tbody');

        for (const bl of BusData) {
            const stations0 = bl.directions[0] ?
                bl.directions[0].stations.join("|") : "";

            const stations1 = bl.directions[0] ?
                bl.directions[0].stations.join("|") : "";

            table.append(
                jQuery("<tr>")
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
}


window.onload = () => {
    var el = document.getElementById('content');
    busfinder.start(el);
};