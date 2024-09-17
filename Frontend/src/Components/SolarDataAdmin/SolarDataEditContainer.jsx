import React, {useEffect, useState} from 'react';

function SolarDataEditContainer({solarData, setIsEditMode, handleSolarDataDelete, setSolarData, fetchCity}) {
    const [date, setDate] = useState(null);
    const [dateForDefault, setDateForDefault] = useState(null);
    const [sunsetHour, setSunsetHour] = useState(null);
    const [sunriseHour, setSunriseHour] = useState(null);
    const [city, setCity] = useState(null);
    
    useEffect(() => {
        const date = new Date(solarData.searchDate);
        setDate(()=> date);
        setDateForDefault(date.toISOString().substring(0,10));
        console.log(date);
        const fetchAndSetCity = async () => {
            const cityData = await fetchCity(solarData.cityId);
            setCity(cityData);
        };
        fetchAndSetCity();
    }, []);
    
    ///City/GetByName-en should make you able to change id while seeing and using the name on the frontend- if the city doesn't exist in the db yet, it should give an error
    return (
        <div className='cityDisplayCont'>
            <form className='cityDisplayFormContents'>
                <label htmlFor="searchDate">Date searched:</label>
                <input id="searchDate" type="date" defaultValue={dateForDefault} onChange={(e)=> setDate(e.target.value)}></input>
                
                <label></label>
                <input></input>
            </form>
        </div>
);
}

export default SolarDataEditContainer;