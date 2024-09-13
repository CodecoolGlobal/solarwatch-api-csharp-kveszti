import React, {useState} from 'react';

function CityEditContainer({city, setIsEditMode, handleCityDelete, setCities}) {
const [name, setName] = useState(city.name);
const [country, setCountry] = useState(city.country);
const [state, setState] = useState(city.state);
const [latitude, setLatitude] = useState(city.latitude);
const [longitude, setLongitude] = useState(city.longitude);

    async function handleCityUpdate(){
        const url = `api/City/UpdateCity`;
        const token = localStorage.getItem('token');
        try {
            const response = await fetch(url,{
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${token}`
                },
                body:JSON.stringify({
                    id: city.id,
                    name: name,
                    country: country,
                    state: state,
                    latitude: parseFloat(latitude),
                    longitude: parseFloat(longitude)
                })
            });

                if(response.ok){
                    setCities((cities) => {
                        return cities.map(cityMap => {
                            if (cityMap.id === city.id) {
                                cityMap.name = name;
                                cityMap.country = country;
                                cityMap.state = state;
                                cityMap.latitude = parseFloat(latitude);
                                cityMap.longitude = parseFloat(longitude);
                            }
                            return cityMap;
                        });
                    })
                    setIsEditMode(() => null)
                } 
            } catch(e) {
                console.log(e, "Something went wrong while updating the city...")
                }
    }
    return (
        <div className='cityDisplayCont'>
            <form className='cityDisplayFormContents'>
                <label htmlFor='cityName'>Name: </label>
                <input id='cityName' type="text" defaultValue={city.name} onChange={(e) => setName(e.target.value)}></input>
                <label htmlFor='country'>Country: </label>
                <input id='contry' type="text" defaultValue={city.country} onChange={(e) => setCountry(e.target.value)}></input>
                <label htmlFor='state'>State: </label>
                <input id='state' type="text" defaultValue={city.state} onChange={(e) => setState(e.target.value)}></input>
                <label htmlFor='country'>Latitude: </label>
                <input id='latitude' type="text" defaultValue={city.latitude} onChange={(e) => setLatitude(e.target.value)}></input>
                <label htmlFor='country'>Longitude: </label>
                <input id='longitude' type="text" defaultValue={city.longitude} onChange={(e) => setLongitude(e.target.value)}></input>
            </form>
            <div className='actionButtons'>
                <button className='deleteButton' onClick={() => handleCityDelete(city.id)}>🗑️</button>
                <button className='editButton' onClick={()=> handleCityUpdate()}>✔️</button>
            </div>
        </div>
    );
}

export default CityEditContainer;