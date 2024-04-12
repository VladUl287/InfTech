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
        const subFolders = folder.find('>.folders')
        const subFiles = folder.find('>.files')
        $.get('/Folder/Get?folderId=' + folderId)
            .then(folders => subFolders.html(folders))
        $.get('/File/Get?folderId=' + folderId)
            .then(files => subFiles.html(files))
    }
}

function checkFolderActionMode(folderId, parentFolderId) {
    switch (actionMode.mode) {
        case mode.deleteFolder:
            $.ajax({
                url: '/Folder/Delete?folderId=' + folderId,
                type: 'DELETE',
                success: () => loadFolders(parentFolderId)
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

function loadFolders(folderId) {
    if (folderId) {
        const folder = $('.folder-id-' + folderId)
        if (folder.hasClass(open) || folder.hasClass(opened)) {
            const subFolders = folder.find('>.folders')
            $.get('/Folder/Get?folderId=' + folderId)
                .then(folders => subFolders.html(folders))
        }
        return
    }
    const foldersWrap = $('.tree>.folders').empty()
    $.get('/Folder/Get').then(folders => foldersWrap.html(folders))
}

function loadFiles(folderId) {
    if (folderId) {
        const folder = $('.folder-id-' + folderId)
        if (folder.hasClass(open) || folder.hasClass(opened)) {
            const subFiles = folder.find('>.files')
            $.get('/File/Get?folderId=' + folderId)
                .then(files => subFiles.html(files))
        }
        return
    }
    const filesWrap = $('.tree>.files').empty()
    $.get('/File/Get').then(files => filesWrap.html(files))
}
