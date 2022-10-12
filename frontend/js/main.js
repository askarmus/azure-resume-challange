window.addEventListener('DOMContentLoaded', (event) => {
  getVisitCount();
});


const functionApi = 'https://getaskarresumecount.azurewebsites.net/api/GetAskarResumeCount?'; 

const getVisitCount = () => {
  let count = 0;
  fetch(functionApi)
  .then(response => {
      return response.json()
  })
  .then(response => {
      count = response.totalCount;
      document.getElementById('counter').innerText = count;
  }).catch(function(error) {
      console.log(error);
    });
  return count;
}