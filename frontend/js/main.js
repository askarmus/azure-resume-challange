
const apiURL = '';

function getcount() {
    
fetch(apiURL)
  .then(function( res) {
    document.getElementById('counter').innerText = res.counter;
  })
}
