function rootEvent() {
    if (actionMode.mode === mode.createFolder) {
        $.get('/Folder/Create').then(setModalContent)
        actionMode.mode = undefined
        showModal()
    }
    else if (actionMode.mode === mode.uploadFile) {
        $.get('/File/Create').then(setModalContent)
        actionMode.mode = undefined
        showModal()
    }
}
