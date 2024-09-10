import React, {useEffect, useState} from 'react';

function SolarDataDisplayContainer({solarData, setIsEditMode, handleSolarDataDelete}) {
    const [date, setDate] = useState(null);
    const [sunsetHour, setSunsetHour] = useState(null);
    const [sunriseHour, setSunriseHour] = useState(null);
    const [city, setCity] = useState(null);
    
    function createDateString(type, date){
        const day = `${date.getFullYear()}. ${date.getMonth() + 1 > 10 ? '' : '0'}${date.getMonth() + 1}. ${date.getDay() + 1 > 10 ? '' : '0'}${date.getDay() + 1}.`;
        
        return type === "withTime" ? day + ` ${date.getHours()}:${date.getMinutes()}` : day;
    };

    useEffect(() => {
        const dateForString = new Date(solarData.searchDate); 
        
     const dayString = createDateString("withoutTime", dateForString);
     
       setDate(()=> dayString);
       
       const sunsetForString = new Date (solarData.sunset);
       const sunriseForString = new Date(solarData.sunrise);
       
       const sunriseString = createDateString("withTime", sunriseForString);
       const sunsetString = createDateString("withTime", sunsetForString);
       
       setSunriseHour(()=>sunriseString);
       setSunsetHour(()=> sunsetString)
        
       async function fetchCity(){
           const url = `api/City/GetById?id=${solarData.cityId}`;
           const token = localStorage.getItem('token');
           const response = await fetch(url,{
               method: 'GET',
               headers: {
                   'Content-Type': 'application/json',
                   'Authorization': `Bearer ${token}`
               }
           });
     
           const data = await response.json();
           setCity(data);
        };
       fetchCity()
    }, []);
    
    
    
    return city ? (<div className="cityDisplayCont">
                <div className='cityDisplayTextContents'>
                    <h3>{date} {city.name}</h3>
                    <div>Timezone: {solarData.timeZone}</div>
                    <div>Sunrise: {sunriseHour}</div>
                    <div>Sunset: {sunsetHour}</div>
                </div>
                <div className='actionButtons'>
                    <button className='deleteButton' onClick={() => handleSolarDataDelete(solarData.id)}>🗑️</button>
                    <button className='editButton' onClick={() => setIsEditMode(() => true)}>✏️</button>
                </div>
            </div> ) 
        : <div>Loading...</div>;
}

export default SolarDataDisplayContainer;