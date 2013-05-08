$(document).ready(function () {
    $("#tabstrip").kendoTabStrip({
        animation: { open: { effects: "fadeIn" } },
        contentUrls: [
                    '/Admin/Admin/PostGrid',
                    '/Admin/Admin/CategoryGrid',
                    '/Admin/Admin/TagGrid'
        ]
    });
});