window.startAuthMonitor = function () {
    setInterval(async () => {
        try {
            const res = await fetch('/auth/ping', { credentials: 'include' });
            if (res.status === 401) {
                const ret = encodeURIComponent(location.pathname + location.search);
                location.href = '/login?ReturnUrl=' + ret;
            }
        } catch { /* ignore */ }
    }, 5000);
};
