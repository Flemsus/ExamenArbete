
export function isAuthenticated() {
    const token = localStorage.getItem('loggedInUser');
    return !!token;
}