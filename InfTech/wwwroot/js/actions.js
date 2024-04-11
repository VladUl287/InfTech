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
        target[prop] !== undefined && toggleActive('#action-' + target[prop], false)
        value !== undefined && toggleActive('#action-' + value, true)
        target[prop] = value
    }
})

function toggleActive(selector, value) {
    const btn = document.querySelector(selector)
    btn?.classList.toggle("active", value)
}

function setActionMode(mode) {
    actionMode.mode = actionMode.mode === mode ? undefined : mode
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