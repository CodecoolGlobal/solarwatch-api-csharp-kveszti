import React, {useEffect, useState} from 'react';
import CityDisplayContainer from "./CityDisplayContainer.jsx";
import CityEditContainer from "./CityEditContainer.jsx";

function CityDisplay({setIsEditMode, isEditMode}) {
    const [cities, setCities] = useState(null);

    useEffect(() => {
        async function fetchCities(){
            const url = "api/City/GetAllCities";
            const token = localStorage.getItem('token');
            const response = await fetch(url,{
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${token}`
                }
            });
            const data = await response.json();
            setCities(data);
        }
        fetchCities();
    }, []);
    
    async function handleCityDelete(id){
        const url = `api/City/DeleteCity?id=${id}`;
        const token = localStorage.getItem('token');

        const response = await fetch(url,{
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            }
        });
        
        if(response.ok){
            const newFilteredArr = cities.filter(city => city.id !== id);
            setCities(() => newFilteredArr);
        }else {
            console.log("Something went wrong while deleting the city...")
        }
    }
    
    
    return (
       <div className='containerDiv'>
           {cities ? (cities.sort((a,b) => a.name.localeCompare(b.name)).map(city => isEditMode === city.id ?  <CityEditContainer key={city.id} city={city} setIsEditMode={setIsEditMode} handleCityDelete={handleCityDelete}/> : <CityDisplayContainer key={city.id} city={city} setIsEditMode={setIsEditMode} handleCityDelete={handleCityDelete}/>)) : <div>Loading...</div>}
    </div>
    );  
}

export default CityDisplay;