function clickOnFolder(folderId) {
    if (checkFolderActionMode(folderId)) {
        actionMode.mode = undefined
        return
    }
    const open = 'open'
    const opened = 'opened'
    const treeItem = $('.folder-id-' + folderId)
    treeItem?.toggleClass(open)

    if (treeItem?.hasClass(open) && !treeItem.hasClass(opened)) {
        treeItem.addClass(opened)
        const contentFolders = treeItem.find('.folders')
        const contentFiles = treeItem.find('.files')
        $.when(
            $.get('/Folder/Get?folderId=' + folderId),
            $.get('/File/Get?folderId=' + folderId))
            .then((folders, files) => {
                contentFolders.append(folders[0])
                contentFiles.append(files[0])
            })
    }
}

function checkFolderActionMode(folderId) {
    switch (actionMode.mode) {
        case mode.deleteFolder:
            $.ajax({
                url: '/Folder/Delete?folderId=' + folderId,
                type: 'DELETE',
                success: reloadTree
            })
            break
        case mode.createFolder:
            showModal()
            $.get('/Folder/Create?folderId=' + folderId)
                .then(result => setModalContent(result))
            break
        case mode.uploadFile:
            showModal()
            $.get('/File/Create?folderId=' + folderId)
                .then(result => setModalContent(result))
            break
        case mode.rename:
            const target = document.querySelector('.folder-id-' + folderId + ' .name')
            target?.setAttribute('contenteditable', true)
            target?.focus()
            target?.addEventListener('focusout', () => {
                target.setAttribute('contenteditable', false)
                $.ajax({
                    url: '/Folder/Rename?id=' + folderId + '&name=' + target.innerText,
                    type: 'PUT',
                })
            }, { once: true })
            break
        default:
            return false
    }
    return true
}