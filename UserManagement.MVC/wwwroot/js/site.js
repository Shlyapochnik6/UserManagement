let checkbox = document.getElementById('all-checks');
checkbox.addEventListener("change", function () {
    GetAllBoxes(checkbox.checked);
});
function GetAllBoxes(c) {
    let checkboxes = document.querySelectorAll('input[type=checkbox]');

    checkboxes.forEach((checkbox) => {
        checkbox.checked = c;
    })
}

function getCheckedUsers() {
    let checkboxes = document.getElementsByClassName('chboxes')
    let checkedUsers = []
    let checked = []
    for (let index = 0; index < checkboxes.length; index++) {
        if (checkboxes[index].checked) {
            checked.push(checkboxes[index].value);
        }
    }
    let a = new Set(checked);
    a.forEach(x => checkedUsers.push(x));
    return checkedUsers;
}

async function SendServer(input, data) {
    if (getCheckedUsers().length > 0){
        await fetch(input, {
            method: 'POST',
            body: JSON.stringify(data),
            headers: {
                "Content-Type": "application/json"
            }
        });
        document.location.reload();
    }
}

let bBtn = document.getElementById('block-btn')
bBtn.addEventListener('click', async function (){
    await SendServer("AdminPanel/Block", getCheckedUsers())
});

let unbBtn = document.getElementById('unblock-btn')
unbBtn.addEventListener('click', async function (){
    await SendServer("AdminPanel/Unblock", getCheckedUsers())
});

let dBtn = document.getElementById('delete-btn')
dBtn.addEventListener('click', async function (){
    await SendServer("AdminPanel/Remove", getCheckedUsers())
});
