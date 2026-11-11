/* ==========================================================================
   Zekr Hakeem — UI core helpers (header, toasts, confirms, formatting)
   ========================================================================== */
(function () {
    'use strict';

    // ---------- header: shadow on scroll + mobile panel
    const header = document.getElementById('zhHeader');
    const onScroll = () => header && header.classList.toggle('scrolled', window.scrollY > 8);
    document.addEventListener('scroll', onScroll, { passive: true });
    onScroll();

    const nav = document.getElementById('zhNav');
    const backdrop = document.getElementById('zhNavBackdrop');
    const setMenu = open => {
        nav && nav.classList.toggle('open', open);
        backdrop && backdrop.classList.toggle('open', open);
        document.body.style.overflow = open ? 'hidden' : '';
    };
    document.getElementById('zhMenuToggle')?.addEventListener('click', () => setMenu(true));
    document.getElementById('zhMenuClose')?.addEventListener('click', () => setMenu(false));
    backdrop?.addEventListener('click', () => setMenu(false));
    document.addEventListener('keydown', e => { if (e.key === 'Escape') setMenu(false); });

    // ---------- helpers
    const esc = s => (s ?? '').toString().replace(/[&<>"']/g, c => ({ '&': '&amp;', '<': '&lt;', '>': '&gt;', '"': '&quot;', "'": '&#39;' }[c]));

    const palette = [['#0F5E4F', '#15B392'], ['#1D7AA8', '#54C3E8'], ['#B5487E', '#E58BB5'], ['#C9A227', '#E9C85A'], ['#5B4BB7', '#9B8CF0'], ['#C2571A', '#F09A5E'], ['#2E7D32', '#7CC47F']];
    const hash = s => { let h = 0; for (const c of (s || '')) h = (h * 31 + c.charCodeAt(0)) | 0; return Math.abs(h); };
    const initials = name => (name || '?').trim().split(/\s+/).slice(0, 2).map(w => w[0]).join('');

    function avatar(user, size = '', withPresence = true) {
        const [a1, a2] = palette[hash(user.id || user.fullName) % palette.length];
        const img = user.hasPicture ? `<img src="/Users/Avatar/${encodeURIComponent(user.id)}" alt="" loading="lazy" onerror="this.remove()">` : esc(initials(user.fullName));
        const presence = withPresence ? `<span class="presence ${user.isOnline ? 'on' : ''}" data-presence="${esc(user.id)}"></span>` : '';
        return `<span class="zh-avatar ${size}" style="--a1:${a1};--a2:${a2}">${img}${presence}</span>`;
    }

    const rtf = new Intl.RelativeTimeFormat('ar', { numeric: 'auto' });
    function timeAgo(value) {
        if (!value) return 'لم يسجل دخول';
        const d = new Date(value.endsWith && !value.endsWith('Z') && !/[+-]\d\d:\d\d$/.test(value) ? value + 'Z' : value);
        const diff = (d - new Date()) / 1000;
        const units = [['year', 31536000], ['month', 2592000], ['week', 604800], ['day', 86400], ['hour', 3600], ['minute', 60]];
        for (const [u, s] of units) if (Math.abs(diff) >= s) return rtf.format(Math.round(diff / s), u);
        return 'الآن';
    }
    function formatDate(value, withTime = false) {
        if (!value) return '—';
        const d = new Date(value.endsWith && !value.endsWith('Z') && !/[+-]\d\d:\d\d$/.test(value) ? value + 'Z' : value);
        return d.toLocaleString('ar-EG', withTime ? { dateStyle: 'medium', timeStyle: 'short' } : { dateStyle: 'medium' });
    }

    function toast(title, message = '', type = 'success', timeout = 4200) {
        const box = document.getElementById('zhToasts');
        if (!box) return;
        const map = { success: ['#15B392', 'bi-check-circle-fill'], error: ['#D64545', 'bi-x-octagon-fill'], warning: ['#E5A023', 'bi-exclamation-triangle-fill'], info: ['#1D7AA8', 'bi-info-circle-fill'] };
        const [c, icon] = map[type] || map.info;
        const el = document.createElement('div');
        el.className = 'zh-toast';
        el.style.setProperty('--c', c);
        el.innerHTML = `<i class="bi ${icon}"></i><div class="flex-grow-1"><b>${esc(title)}</b>${message ? `<small>${esc(message)}</small>` : ''}</div><button class="btn-close btn-sm" aria-label="إغلاق"></button>`;
        const close = () => { el.classList.add('out'); setTimeout(() => el.remove(), 300); };
        el.querySelector('button').onclick = close;
        box.prepend(el);
        if (timeout) setTimeout(close, timeout);
    }

    async function confirm({ title, text, confirmText = 'تأكيد', danger = false, icon = 'warning', input = null }) {
        if (!window.Swal) return { isConfirmed: window.confirm(title + '\n' + (text || '')) };
        return Swal.fire({
            title, html: text, icon, input, showCancelButton: true,
            confirmButtonText: confirmText, cancelButtonText: 'إلغاء', reverseButtons: true,
            customClass: { popup: 'zh-swal', confirmButton: 'zh-btn ' + (danger ? 'zh-btn-danger' : 'zh-btn-primary'), cancelButton: 'zh-btn zh-btn-light' },
            buttonsStyling: false, focusCancel: danger
        });
    }

    const debounce = (fn, ms = 300) => { let t; return (...a) => { clearTimeout(t); t = setTimeout(() => fn(...a), ms); }; };

    function animateNumber(el, to) {
        if (!el) return;
        const from = parseInt(el.textContent.replace(/\D/g, '')) || 0;
        if (from === to) { el.textContent = to.toLocaleString('ar-EG'); return; }
        const start = performance.now(), dur = 600;
        const step = now => {
            const p = Math.min(1, (now - start) / dur), e = 1 - Math.pow(1 - p, 3);
            el.textContent = Math.round(from + (to - from) * e).toLocaleString('ar-EG');
            if (p < 1) requestAnimationFrame(step);
        };
        requestAnimationFrame(step);
    }

    function copy(text) {
        navigator.clipboard?.writeText(text).then(() => toast('تم النسخ', '', 'success', 1800));
    }

    // TempData flash messages (MsgType/MsgTitle/Msg) -> toast
    document.addEventListener('DOMContentLoaded', () => {
        document.querySelectorAll('[data-zh-flash]').forEach(el => toast(el.dataset.title || '', el.dataset.message || '', el.dataset.type || 'success'));
    });

    window.ZH = { esc, avatar, initials, timeAgo, formatDate, toast, confirm, debounce, animateNumber, copy };
})();
