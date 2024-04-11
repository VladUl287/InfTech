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

function clickOnFile(fileId, fileName) {
    if (checkFileActionMode(fileId, fileName)) {
        actionMode.mode = undefined
        return
    }
    addTab(fileId, fileName)
}

function checkFileActionMode(fileId, fileName) {
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
                success: reloadTree
            });
            break
        case mode.rename:
            const target = document.querySelector('.file-id-' + fileId + '>.name')
            target?.setAttribute('contenteditable', true)
            target?.focus()
            target?.addEventListener('focusout', () => {
                target?.setAttribute('contenteditable', false)
                $.ajax({
                    url: '/File/Rename?id=' + fileId + '&name=' + target.innerText,
                    type: 'PUT'
                })
            }, { once: true })
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