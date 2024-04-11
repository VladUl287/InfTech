function addTab(fileId, fileName) {
    const tab = $('#tab-nav-wrapper>#tab-' + fileId)
    if (!tab.length) {
        appendTab(fileId, fileName)
        loadTabContent(fileId)
    }
}

function appendTab(fileId, fileName) {
    $.get('/Main/Tab?fileId=' + fileId + '&name=' + fileName)
        .then(result => $('#tab-nav-wrapper').append(result))
}

function loadTabContent(fileId) {
    $('#tab-content-wrapper').load('/Main/TabContent?fileId=' + fileId)
}

function closeTab(event, fileId) {
    event?.stopPropagation()
    $('#tab-nav-wrapper>#tab-' + fileId).remove()
    $('#tab-content-wrapper>#tab-' + fileId).remove()
}