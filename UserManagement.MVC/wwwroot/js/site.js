let dBtn = document.getElementById("d-btn");
dBtn.addEventListener('click', async function() {
    await SendServer("AdminPanel/Remove", getSelectedUsers());
});

let bBtn = document.getElementById('b-btn')
bBtn.addEventListener('click', async function (){
    await SendServer("AdminPanel/Block", getSelectedUsers())
});
let unbBtn = document.getElementById('unb-btn')
unbBtn.addEventListener('click', async function (){
    await SendServer("AdminPanel/Unblock", getSelectedUsers())
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
