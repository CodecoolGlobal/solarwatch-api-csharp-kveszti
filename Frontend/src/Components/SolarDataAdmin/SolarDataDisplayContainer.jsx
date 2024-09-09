import React, {useEffect, useState} from 'react';

function SolarDataDisplayContainer({solarData, setIsEditMode, handleSolarDataDelete}) {
    const [date, setDate] = useState(null);
    const [sunsetHour, setSunsetHour] = useState(null);
    const [sunriseHour, setSunriseHour] = useState(null);

    useEffect(() => {
        const dateForString = new Date(solarData.sunset);
        
     const dayString = `${dateForString.getFullYear()}. ${dateForString.getMonth() + 1 > 10 ? '' : '0'}${dateForString.getMonth() + 1}. ${dateForString.getDay() + 1 > 10 ? '' : '0'}${dateForString.getDay() + 1}.`;
     
       setDate(()=> dayString);
       
       const sunsetForString = new Date (solarData.sunset);
       const sunriseForString = new Date(solarData.sunrise);
       
       const sunriseString = `${sunriseForString.getFullYear()}. ${sunriseForString.getMonth() + 1 > 10 ? '' : '0'}${sunriseForString.getMonth() + 1}. ${sunriseForString.getDay() + 1 > 10 ? '' : '0'}${sunriseForString.getDay() + 1}. ${sunriseForString.getHours()}:${sunriseForString.getMinutes()}`;
       
       const sunsetString = `${sunsetForString.getFullYear()}. ${sunsetForString.getMonth() + 1 > 10 ? '' : '0'}${sunsetForString.getMonth() + 1}. ${sunsetForString.getDay() + 1 > 10 ? '' : '0'}${sunsetForString.getDay() + 1}. ${sunsetForString.getHours()}:${sunsetForString.getMinutes()}`;
       
       setSunriseHour(()=>sunriseString);
       setSunsetHour(()=> sunsetString)
    }, []);
    
    
    
    return (
        <div className="cityDisplayCont">
            <div className='cityDisplayTextContents'>
                <h3>{date}</h3>
                <div>Timezone: {solarData.timeZone}</div>
                <div>Sunrise: {sunriseHour}</div>
                <div>Sunset: {sunsetHour}</div>
            </div>
            <div className='actionButtons'>
                <button className='deleteButton' onClick={() => handleSolarDataDelete(solarData.id)}>🗑️</button>
                <button className='editButton' onClick={() => setIsEditMode(() => true)}>✏️</button>
            </div>
        </div>
    );
}

export default SolarDataDisplayContainer;