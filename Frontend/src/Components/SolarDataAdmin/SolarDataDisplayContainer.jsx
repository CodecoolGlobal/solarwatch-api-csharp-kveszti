import React, {useEffect, useState} from 'react';

function SolarDataDisplayContainer({solarData, setIsEditMode, handleSolarDataDelete, fetchCity}) {
    const [searchDate, setSearchDate] = useState(null);
    const [sunsetHour, setSunsetHour] = useState(null);
    const [sunriseHour, setSunriseHour] = useState(null);
    const [city, setCity] = useState(null);
    
    function createDateString(type, date){
        const day = `${date.getFullYear()}. ${date.getMonth() + 1 > 10 ? '' : '0'}${date.getMonth() + 1}. ${date.getDate() >= 10 ? '' : '0'}${date.getDate()}.`;
        
        return type === "withTime" ? day + ` ${date.getHours()}:${date.getMinutes() >= 10 ? '' : '0'}${date.getMinutes()}` : day;
    }

    useEffect(() => {
        const dateForString = new Date(solarData.searchDate); 
        console.log(dateForString);
     const dayString = createDateString("withoutTime", dateForString);
     console.log(dayString);
       setSearchDate(()=> dayString);
       
       const sunsetForString = new Date (solarData.sunset);
       const sunriseForString = new Date(solarData.sunrise);
       
       const sunriseString = createDateString("withTime", sunriseForString);
       const sunsetString = createDateString("withTime", sunsetForString);
       
       setSunriseHour(()=>sunriseString);
       setSunsetHour(()=> sunsetString)

        const fetchAndSetCity = async () => {
            const cityData = await fetchCity(solarData.cityId);
            setCity(cityData);
        };
       fetchAndSetCity();
       
    }, []);

    useEffect(() => {
        if(city){
            console.log(city);
        }
        
    }, [city]);
    
    
    
    return city ? (<div className="cityDisplayCont">
                <div className='cityDisplayTextContents'>
                    <h3>{searchDate} {city.name}</h3>
                    <div>Timezone: {solarData.timeZone}</div>
                    <div>Sunrise: {sunriseHour}</div>
                    <div>Sunset: {sunsetHour}</div>
                </div>
                <div className='actionButtons'>
                    <button className='deleteButton' onClick={() => handleSolarDataDelete(solarData.id)}>🗑️</button>
                    <button className='editButton' onClick={() => setIsEditMode(() => solarData.id)}>✏️</button>
                </div>
            </div> ) 
        : <div>Loading...</div>;
}

export default SolarDataDisplayContainer;