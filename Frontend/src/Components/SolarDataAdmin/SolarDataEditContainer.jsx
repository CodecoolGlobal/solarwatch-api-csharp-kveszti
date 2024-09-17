import React, {useEffect, useState} from 'react';

function SolarDataEditContainer({solarData, setIsEditMode, handleSolarDataDelete, setSolarData, fetchCity}) {
    const [date, setDate] = useState(null);
    const [dateForDefault, setDateForDefault] = useState(null);
    const [sunset, setSunset] = useState(null);
    const [sunsetForDefault, setSunsetForDefault] = useState(null);
    const [sunrise, setSunrise] = useState(null);
    const [sunriseForDefault, setSunriseForDefault] = useState(null);
    const [city, setCity] = useState(null);
    const [modifiedCity, setModifiedCity] = useState(null);
    const [isCityError, setIsCityError] = useState(false);
    const [cityErrorText, setCityErrorText] = useState(null);

    const formatDateTimeLocal = (date) => {
        const formattedDate = date.toLocaleDateString('en-CA'); 
        const formattedTime = date.toLocaleTimeString('en-GB', {
            hour: '2-digit',
            minute: '2-digit',
            hour12: false,
        });
        return `${formattedDate}T${formattedTime}`;
    };
        
    useEffect(() => {
        const date = new Date(solarData.searchDate);
        setDate(() => date);
        setDateForDefault(date.toLocaleDateString('en-CA'));
        
        const sunrise = new Date(solarData.sunrise);
        setSunrise(()=> sunrise);
        setSunriseForDefault(formatDateTimeLocal(sunrise));
        
        const sunset = new Date(solarData.sunset);
        setSunset(()=> sunset);
        setSunsetForDefault(formatDateTimeLocal(sunset));
        

        const fetchAndSetCity = async () => {
            const cityData = await fetchCity(solarData.cityId);
            setCity(cityData);
        };
        fetchAndSetCity();
    }, []);
    
    async function fetchNewCityByName(name){
        const url = `api/City/GetByName?name=${name}`;
        const token = localStorage.getItem('token');
        try{
            const response = await fetch(url,{
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${token}`
                }});
            console.log(response);
            if(response.ok){
                console.log("hi");
                let cityObject;
                if(response.status === 200){
                     cityObject = await response.json();
                    console.log(cityObject);
                } else {
                    cityObject = null;
                    setCityErrorText("There is no matching city in the db yet, you cannot set this city manually. Update cancelled.")
                    setIsCityError(true);
                }
                return cityObject;
            } else {
                setCityErrorText("Couldn't reach the city in the db, please try another.")
                setIsCityError(true);
            }
            
        }catch(err){
            console.log(err, "Something went wrong while getting city by name...")
            setCityErrorText("You can't set this city, as an error occured while fetching it from the db. Please try something else.");
            setIsCityError(true);
        }
    }
    
    async function handleSolarDataUpdate(){
        setIsCityError(false);
        const url = `api/SolarData/UpdateSolarData`;
        const token = localStorage.getItem('token');
        let cityId;
        if (modifiedCity === null || modifiedCity === city.name){
            cityId = city.id;
        } else {
            const cityObject = await fetchNewCityByName(modifiedCity);
            if (cityObject === null){
                return;
            }
            cityId = cityObject.id;
        }
        
        try {
            const newSolarData = {
                id: solarData.id,
                cityId: cityId,
                sunrise: sunrise,
                sunset: sunset,
                timezone: "Europe/Budapest",
                searchDate: date
            }
            const response = await fetch(url,{
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${token}`
                },
                body:JSON.stringify({...newSolarData})
            });

            if(response.ok){
                setSolarData((solarDatas) => solarDatas.map(sdmap => sdmap.id === solarData.id ? newSolarData : sdmap));
                setIsEditMode(() => null)
            }
        } catch(e) {
            console.log(e, "Something went wrong while updating the city...")
        }
    }
    
    return city ? (<div className='cityDisplayCont'>
        <form className='cityDisplayFormContents'>
            <label htmlFor="city">City:</label>
            <input id='city' type="text" defaultValue={city.name} onChange={(e) => setModifiedCity(e.target.value)}></input>

            {isCityError ? cityErrorText: ''}

            <label htmlFor="searchDate">Date searched:</label>
            <input id="searchDate" type="date" defaultValue={dateForDefault}
                   onChange={(e) => setDate(e.target.value)}></input>

            <label htmlFor='sunrise'>Sunrise:</label>
            <input id='sunrise' type="datetime-local" defaultValue={sunriseForDefault} onChange={(e)=> setSunrise(e.target.value)}></input>

            <label htmlFor='sunset'>Sunset:</label>
            <input id='sunset' type="datetime-local" defaultValue={sunsetForDefault} onChange={(e)=> setSunset(e.target.value)}></input>
        </form>
        <div className='actionButtons'>
            <button className='deleteButton' onClick={() => handleSolarDataDelete(solarData.id)}>🗑️</button>
            <button className='editButton' onClick={() => handleSolarDataUpdate()}>✔️</button>
        </div>
    </div>) : <div>Loading...</div>
}

export default SolarDataEditContainer;