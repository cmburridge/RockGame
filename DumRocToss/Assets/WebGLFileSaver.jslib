mergeInto(LibraryManager.library, {
  DownloadFile: function (dataPtr, filenamePtr) {
    var data = UTF8ToString(dataPtr);
    var filename = UTF8ToString(filenamePtr);
    var blob = new Blob([data], { type: 'application/json' });
    var link = document.createElement('a');
    link.href = window.URL.createObjectURL(blob);
    link.download = filename;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
  },

  OpenFilePicker: function () {
    var fileInput = document.createElement('input');
    fileInput.type = 'file';
    fileInput.accept = '.json';
    fileInput.onchange = function (e) {
        var file = e.target.files[0];
        var reader = new FileReader();
        reader.onload = function (event) {
            // "DataStorageObject" must match the name of your object in the scene 
            // or a script that can route this data.
            SendMessage('SaveManager', 'OnFileLoaded', event.target.result);
        };
        reader.readAsText(file);
    };
    fileInput.click();
  }
});