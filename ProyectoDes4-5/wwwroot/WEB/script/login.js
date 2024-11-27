const users = [
    { correo: "admin@example.com", password: "123456" },
    { correo: "user@example.com", password: "password" }
];

document.getElementById('loginForm').addEventListener('submit', function (e) {
    e.preventDefault();
    const correo = document.getElementById('correo').value;
    const password = document.getElementById('password').value;

    const user = users.find(u => u.correo === correo && u.password === password);
    if (user) {
        localStorage.setItem('authToken', 'simulated-token');
        localStorage.setItem('userEmail', correo);
        window.location.href = 'dashboard.html';
    } else {
        alert('Credenciales incorrectas');
    }
});
