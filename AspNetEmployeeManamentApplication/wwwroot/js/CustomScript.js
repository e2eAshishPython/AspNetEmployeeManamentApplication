function confirmDelete(uniqueID, IsDeletedClicked) {
    var deletespam = "deleteSpan_" + uniqueID;
    var Confimdeletespam = "confirmDeleteSpan_" + uniqueID;
    if (IsDeletedClicked) {
        $('#' + deletespam).hide();
        $('#' + Confimdeletespam).show();
    } else {
        $('#' + deletespam).show();
        $('#' + Confimdeletespam).hide();
    }
}