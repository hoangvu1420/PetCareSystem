import React from "react";
import InformationCard from "./InformationCard";
import { faHeartPulse, faTruckMedical, faTooth } from "@fortawesome/free-solid-svg-icons";
import "../Styles/Info.css";

function Info() {
  return (
    <div className="info-section" id="services">
      <div className="info-title-content">
        <h3 className="info-title">
          <span>What We Do</span>
        </h3>
        <p className="info-description">
          We bring healthcare to your convenience, offering a comprehensive
          range of on-demand medical services tailored to your needs. Our
          platform allows you to connect with experienced online doctors who
          provide expert medical advice, issue online prescriptions, and offer
          quick refills whenever you require them.
        </p>
      </div>

      <div className="info-cards-content">
        <InformationCard
          title="Managing pet"
          description="Easily manage your pet with health.
           Always catch up with their health and codition."
          icon={faTruckMedical}
        />

        <InformationCard
          title="Book a room"
          description="Get your pet taken care of in a precious room"
          icon={faHeartPulse}
        />

        <InformationCard
          title="Book appointment"
          description="Book an appontment for monthly treatments for your pet."
          icon={faTooth}
        />
      </div>
    </div>
  );
}

export default Info;
