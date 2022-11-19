async function SendServer(input, data){
    await fetch(input, {
        method: 'POST',
        body: JSON.stringify(data),
        headers: { "Content-Type": "application/json" }
    });
    document.location.reload();
}
