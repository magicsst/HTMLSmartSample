function changeLocalization() {
    let dateFrom = document.getElementById("dateFrom");
    dateFrom.locale = 'el'

    let dateTo = document.getElementById("dateTo");
    dateTo.locale = 'el'
}

function setGridTooltip() {
    const grid = document.querySelector("smart-grid");
    grid.columns.forEach(col => {
        col.template = function (settings) {
            settings.cell.element.querySelector('div').title = settings.row.data.Notes
        }
    });
}
