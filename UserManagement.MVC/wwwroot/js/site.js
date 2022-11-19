let dBtn = document.getElementById("d-btn");
dBtn.addEventListener('click', async function() {
    await SendServer("AdminPanel/Remove", getSelectedUsers());
});

async function SendServer(input, data){
    await fetch(input, {
        method: 'POST',
        body: JSON.stringify(data),
        headers: { "Content-Type": "application/json" }
    });
    document.location.reload();
}

function getSelectedUsers() {
    let selectedBoxes = [];
    let selectedUsers = [];
    let checkBoxes = document.getElementById("boxes");
    for (let i = 0; i < checkBoxes.length; i++) { 
        if (checkBoxes[i].checked) {
            selectedBoxes.push(checkBoxes[i].value)
        }
    }
    let a = new Set(selectedBoxes);
    a.forEach(x => selectedUsers.push(x));
    return selectedUsers;
}
