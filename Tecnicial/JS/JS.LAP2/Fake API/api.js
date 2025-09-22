const table = document.getElementById("table").getElementsByTagName('tbody')[0];
const xhr = new XMLHttpRequest();
xhr.open("GET", "https://fakestoreapi.com/users");
xhr.onreadystatechange = function() {
    if (xhr.readyState === 4) {
        const data = JSON.parse(xhr.responseText);
        table.innerHTML = '';
        for (let item of data) {
            table.innerHTML += `<tr>
                <td>${item.id}</td>
                <td>${item.name.firstname}</td>
                <td>${item.name.lastname}</td>
                <td>${item.email}</td>
                <td>${item.phone}</td>
                <td>${item.username}</td>
            </tr>`;
        }
    }
};
xhr.send();