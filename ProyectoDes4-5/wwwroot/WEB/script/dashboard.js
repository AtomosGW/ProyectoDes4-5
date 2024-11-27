if (!localStorage.getItem('authToken')) {
    window.location.href = 'login.html';
}

function loadSection(section) {
    fetch(`${section}.html`)
        .then(response => response.text())
        .then(html => {
            document.getElementById('mainContent').innerHTML = html;
        });
}

function logout() {
    localStorage.clear();
    window.location.href = 'login.html';
}
