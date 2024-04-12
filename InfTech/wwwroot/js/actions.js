const mode = Object.freeze({
    createFolder: 0,
    deleteFolder: 1,
    uploadFile: 2,
    downloadFile: 3,
    deleteFile: 4,
    rename: 5,
})

const actionMode = new Proxy({ mode: undefined }, {
    get: (target, prop) => target[prop],
    set(target, prop, value) {
        $(`.action[data-id=${target[prop]}]`).removeClass('active')
        $(`.action[data-id=${value}]`).addClass('active')
        target[prop] = value
    }
})

$(function () {
    setActionClickEvent()
})

function setActionClickEvent() {
    $(document)
        .on('click', '.action', function () {
            const actionId = +$(this).attr('data-id')
            actionMode.mode = actionMode.mode === actionId ? undefined : actionId
        })
}

function showModal() {
    document.querySelector('#myModal')?.showModal()
}

function setModalContent(content) {
    $('#modalContainer').html(content);
}

function closeModal() {
    document.querySelector('#myModal')?.close()
}
