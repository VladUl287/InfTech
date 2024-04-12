$(function () {
    setFileHoverEvents(this)
})

function setFileHoverEvents(document) {
    $(document)
        .on('mouseenter', '.file-tree-item', function () {
            $(this).find('.description').show()
        })
        .on("mouseleave", '.file-tree-item', function () {
            $(this).find('.description').hide()
        })
        .on("mousemove", '.file-tree-item', function (event) {
            $(this).find('.description').css({
                top: event.pageY + 15,
                left: event.pageX + 15
            })
        });
}

function clickOnFile(fileId, fileName, folderId) {
    if (checkFileActionMode(fileId, fileName, folderId)) {
        actionMode.mode = undefined
        return
    }
    addTab(fileId, fileName)
}

function checkFileActionMode(fileId, fileName, folderId) {
    switch (actionMode.mode) {
        case mode.downloadFile:
            fetch('/File/Download?fileId=' + fileId)
                .then(resp => resp.blob())
                .then((blob) => downloadBlob(blob, fileName))
                .catch(() => alert('Ошибка скачивания файла.'))
            break
        case mode.deleteFile:
            $.ajax({
                url: '/File/Delete?fileId=' + fileId,
                type: 'DELETE',
                success: () => loadContent(folderId)
            });
            break
        case mode.rename:
            const target = $('.file-id-' + fileId + '>.name')
            target.attr('contenteditable', true)
            target.trigger('focus')
            target.one('focusout', function () {
                $(this).attr('contenteditable', false)
                $.ajax({
                    url: '/File/Rename?id=' + fileId + '&name=' + this.innerText,
                    type: 'PUT'
                }).always(() => loadContent(folderId))
            })
            break
        default:
            return false
    }
    return true
}

function downloadBlob(blob, fileName) {
    const url = window.URL.createObjectURL(blob)
    const a = document.createElement('a')
    a.download = fileName
    a.href = url
    document.body.appendChild(a)
    a.click()
    window.URL.revokeObjectURL(url)
}
