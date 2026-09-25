for(let i=0;i<9;i++){const h=document.createElement('span');h.className='heart';h.textContent='\u2665';h.style.cssText='left:'+(i*11+3)+'%;animation-duration:'+(14+i*2)+'s;animation-delay:-'+(i*3)+'s';document.body.appendChild(h)}
document.querySelectorAll('nav a').forEach(a=>{if(a.pathname&&a.pathname.toLowerCase()===location.pathname.toLowerCase())a.classList.add('on')});
