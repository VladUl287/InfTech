const open = 'open'
const opened = 'opened'
function clickOnFolder(folderId, parentFolderId) {
    if (checkFolderActionMode(folderId, parentFolderId)) {
        actionMode.mode = undefined
        return
    }

    const folder = $('.folder-id-' + folderId)
    folder?.toggleClass(open)
    if (folder?.hasClass(open) && !folder?.hasClass(opened)) {
        folder.addClass(opened)
        loadContent(folderId)
    }
}

function checkFolderActionMode(folderId, parentFolderId) {
    switch (actionMode.mode) {
        case mode.deleteFolder:
            $.ajax({
                url: '/Folder/Delete?folderId=' + folderId,
                type: 'DELETE',
                success: () => loadContent(parentFolderId)
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
            const target = $('.folder-id-' + folderId + ' .name')
            target.attr('contenteditable', true)
            target.trigger('focus')
            target.one('focusout', function () {
                $(this).attr('contenteditable', false)
                $.ajax({
                    url: '/Folder/Rename?id=' + folderId + '&name=' + this.innerText,
                    type: 'PUT'
                })
            })
            break
        default:
            return false
    }
    return true
}

function loadContent(folderId) {
    if (folderId) {
        const folder = $('.folder-id-' + folderId)
        if (folder.hasClass(open) || folder.hasClass(opened)) {
            const subFolders = folder.find('.folders')
            const subFiles = folder.find('.files')
            $.when(
                $.get('/Folder/Get?folderId=' + folderId),
                $.get('/File/Get?folderId=' + folderId))
                .then((folders, files) => {
                    subFolders.html(folders[0])
                    subFiles.html(files[0])
                })
        }
        return
    }
    const tree = $('.tree').empty()
    $.when(
        $.get('/Folder/Get'),
        $.get('/File/Get'))
        .then((folders, files) => {
            tree.append(folders[0])
            tree.append(files[0])
        })
}
