import React, {useEffect, useState} from 'react';

function SolarDataDisplayContainer({solarData, setIsEditMode, handleSolarDataDelete}) {
    const [date, setDate] = useState(null);
    const [sunsetHour, setSunsetHour] = useState(null);
    const [sunriseHour, setSunriseHour] = useState(null);

    useEffect(() => {
        const dateForString = new Date(solarData.sunset);
        
     const dayString = `${dateForString.getFullYear()}. ${dateForString.getMonth() + 1 > 10 ? '' : '0'}${dateForString.getMonth() + 1}. ${dateForString.getDay() + 1 > 10 ? '' : '0'}${dateForString.getDay() + 1}.`;
     
       setDate(()=> dayString);
    }, []);
    
    
    
    return (
        <div className="solarDataDisplayCont">
            <div className='solarDataDisplayTextContents'>
                <h3>{date}</h3>
                <div></div>
            </div>
            <div className='actionButtons'>
                <button className='deleteButton' onClick={() => handleSolarDataDelete(solarData.id)}>🗑️</button>
                <button className='editButton' onClick={() => setIsEditMode(() => true)}>✏️</button>
            </div>
        </div>
    );
}

export default SolarDataDisplayContainer;