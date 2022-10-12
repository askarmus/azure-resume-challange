window.addEventListener('DOMContentLoaded', (event) => {
  getVisitCount();
});


const functionApi = 'https://challengeresumeforbenny.azurewebsites.net/api/HttpTriggerBenny'; 

const getVisitCount = () => {
  let count = 0;
  fetch(functionApi)
  .then(response => {
      return response.json()
  })
  .then(response => {
      count = response;
      document.getElementById('counter').innerText = count;
  }).catch(function(error) {
      console.log(error);
    });
  return count;
}