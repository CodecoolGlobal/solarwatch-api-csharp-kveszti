import DatePicker from "react-datepicker";
import {useEffect, useState} from "react";
import SearchSolarData from "../Components/SearchSolarData.jsx";
import DisplaySolarData from "../Components/DisplaySolarData.jsx";

export default function SolarWatch({StyledForm, FormLabel, TextInput, SubmitButton, FormContainerDiv}){
    const [isDisplayed, setIsDisplayed] = useState(false);
    const [dataToDisplay, setDataToDisplay] = useState("");
    const [cityToDisplay, setCityToDisplay] = useState("");
    const [typeOfSunData, setTypeOfSunData] = useState("");
    const [isSomethingWrong, setIsSomethingWrong] = useState(false)
    
    async function FetchSolarData(e, city, date, sunData){
      e.preventDefault();
      
      let url = "";
      setIsSomethingWrong(()=> false);
       

        if(sunData === "Sunset"){
            url = `api/SunriseSunset/GetSunset?city=${encodeURIComponent(city)}&timeZone=${encodeURIComponent("Europe/Budapest")}`
            setTypeOfSunData(()=> sunData);
        }else{
          url = `api/SunriseSunset/GetSunrise?city=${encodeURIComponent(city)}&timeZone=${encodeURIComponent("Europe/Budapest")}`;
            setTypeOfSunData(()=> "Sunrise");
      }
        if (date) {
            url += `&date=${encodeURIComponent(date)}`;
        }


        const token = localStorage.getItem('token');
    
        const response = await fetch(url, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            }
        });
       
        if (response.ok) {
            const data = await response.json();
            setCityToDisplay(() => city);
            setDataToDisplay(()=> data);}
        
        else {
            setIsSomethingWrong(true);
        }
    }

    useEffect(() => {
        if(typeOfSunData != ""){
            setIsDisplayed(() => true);
        }
    }, [typeOfSunData]);
    
    return(
        <> {isDisplayed? <DisplaySolarData dataToDisplay={dataToDisplay} cityToDisplay={cityToDisplay} typeOfSunData={typeOfSunData} setIsDisplayed={setIsDisplayed}/>: <SearchSolarData StyledForm={StyledForm} FormLabel={FormLabel} TextInput={TextInput} SubmitButton={SubmitButton} FormContainerDiv={FormContainerDiv} fetchSolarData={FetchSolarData} isSomethingWrong={isSomethingWrong}/>}
        </>
    )
}