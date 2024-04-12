function reloadTree() {
    const tree = $('.tree').empty()
    $.when($.get('/Folder/Get'), $.get('/File/Get'))
        .then((folders, files) => {
            tree.append(folders[0])
            tree.append(files[0])
        })
}

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
